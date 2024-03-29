﻿using API.Models;
using API.Services;
using API.Utils.Helper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace API.Controllers
{
    /// <summary>
    /// User controller.
    /// </summary>
    [Authorize]
    public class UserController : ApiController
    {
        private UserService _userService = new UserService();
        private Google _google = new Google();
        private HCaptcha _hCaptcha = new HCaptcha();

        // POST: api/user/login
        /// <summary>
        /// Returns user login response
        /// </summary>
        /// <returns>The login response.</returns>
        /// <param name="userLogin">User login.</param>
        /// <response code="200">{ tokenType: string, accessToken: string }</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/login")]
        public async Task<object> Login([FromBody]UserLogin userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            // Google Recaptcha Validation
            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {
                GoogleResponse googleResponse = await _google.ValidateRecaptcha<GoogleResponse>(userLogin.Token);
                if (!googleResponse.Success)
                    return BadRequest("error.validation.invalid-recaptcha");
            }

            // HCaptcha Validation
            if (WebConfigurationManager.AppSettings["HCaptcha"] == "true")
            {
                HCaptchaResponse hCaptchaResponse = await _hCaptcha.Validate<HCaptchaResponse>(userLogin.Token);
                if (!hCaptchaResponse.Success)
                    return BadRequest("error.validation.invalid-hcaptcha");
            }

            object user = await _userService.Auth(userLogin);
            if (user == null)
                return BadRequest("error.validation.incorrect-login");

            return Ok(user);
        }

        // POST: api/user/register
        /// <summary>
        /// Register the specified userRegister.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="userRegister">User register.</param>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/register")]
        public async Task<object> Register([FromBody]UserRegister userRegister)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            // Google Recaptcha Validation
            if (WebConfigurationManager.AppSettings["GoogleRecaptcha"] == "true")
            {
                GoogleResponse googleResponse = await _google.ValidateRecaptcha<GoogleResponse>(userRegister.Token);
                if (!googleResponse.Success)
                    return BadRequest("error.validation.invalid-recaptcha");
            }

            // HCaptcha Validation
            if (WebConfigurationManager.AppSettings["HCaptcha"] == "true")
            {
                HCaptchaResponse hCaptchaResponse = await _hCaptcha.Validate<HCaptchaResponse>(userRegister.Token);
                if (!hCaptchaResponse.Success)
                    return BadRequest("error.validation.invalid-hcaptcha");
            }

            UserRegisterResponse user = await _userService.Register(userRegister);

            if (user.ErrorEmail)
                return BadRequest("error.user.email-exists");

            if (user.ErrorCpf)
                return BadRequest("error.user.cpf-exists");

            // Register Mail
            if (WebConfigurationManager.AppSettings["Mail"] == "true")
            {
                string message = RenderRazor.RenderView("~/Views/MailTemplates/Register.cshtml", userRegister, null);
                MailService.SendMailAsync(userRegister.Email, new string[] { }, "Register", message);
            }


            return Ok(user);
        }

        // GET: api/users
        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The user list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        [HttpGet]
        [Route("api/users")]
        [Authorize(Roles = "Admin")]
        public async Task<object> List([FromUri]SearchParams searchParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            return Ok(await _userService.List(searchParams));
        }

        // POST: api/user
        /// <summary>
        /// Add user in Admin
        /// </summary>
        /// <returns>The added user.</returns>
        [HttpPost]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Add([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            UserAddResponse addUser = await _userService.Add(user);

            if (addUser.ErrorEmail)
                return BadRequest("error.user.email-exists");

            if (addUser.ErrorCpf)
                return BadRequest("error.user.cpf-exists");

            return Ok(addUser);
        }

        // PUT: api/user
        /// <summary>
        /// Edit user in admin.
        /// </summary>
        /// <returns>The edited user.</returns>
        [HttpPut]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Edit([FromBody]User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(start => start.Errors).Select(error => error.ErrorMessage).Take(1).ElementAt(0));

            UserEditResponse editUser = await _userService.Edit(user);

            if (editUser.ErrorId)
                return BadRequest("error.user.invalid-id");

            if (editUser.ErrorEmail)
                return BadRequest("error.user.email-exists");

            if (editUser.ErrorCpf)
                return BadRequest("error.user.cpf-exists");

            return Ok(editUser);
        }

        // DELETE: api/user/{id:int}
        /// <summary>
        /// Delete the specified user id.
        /// </summary>
        /// <returns>The id.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete]
        [Route("api/user")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Delete([FromBody]int id)
        {
            return Ok(await _userService.Delete(id));
        }
    }
}
