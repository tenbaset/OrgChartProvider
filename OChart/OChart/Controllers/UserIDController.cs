using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OChart.Controllers {
    public class UserIDController : ApiController {

        /// <summary>
        /// Get the user ID for the currently logged on user
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Here so the user ID can be passed to the ADInfoProvider.  This can then be used to start
        /// the chart with the current user as the start point.
        /// </remarks>
        [Route("ad/GetId")]
        [HttpGet]
        public string GetId() {
            if (User.Identity.IsAuthenticated) {
                return User.Identity.Name;
            } else {
                return string.Empty;
            }
        }
    }
}
