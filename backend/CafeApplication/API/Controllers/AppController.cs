﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Account;

namespace API.Controllers {
    [ApiController]
    public class AppController : ControllerBase {

        [HttpPost]
        [Route("Login")]
        public StatusCodeResult ValidateCredentials([FromBody]AccountCredentials data) {

           if (AccountValidator.compareCredentials(data.email, data.password) == true) {
                return StatusCode(200);
            } else {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [Route("CreateAccount")]
        public HttpResponseMessage AddNewUser([FromBody]AccountCredentials data) {
            //TODO: use factory to create object instance
            AccountCreator c = new AccountCreator();

            //TODO: check if any fields are null -> return proper response code
            

            if (c.storeNewAccount(data.userID, data.firstName, data.lastName, data.email, data.password))
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }

    }


    public class AccountCredentials {
        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}