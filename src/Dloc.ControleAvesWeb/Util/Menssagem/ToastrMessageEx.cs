using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace DLOC.ControleAvesWeb.Util.Menssagem
{

    public static class ToastrMessageEx
    {
        static string _defaultCookieName = "Ave-Error-Message";

        public static string DefaultCookieName { get => _defaultCookieName; }

        private enum ToastrDialogType : short
        {
            Info = 0,
            Success = 1,
            Warning = 2,
            Error = 3
        }

        public static void ChangeDefaultCookieName(string name)
        {
            _defaultCookieName = name;
        }

        public static ActionResult ShowMessage(this ControllerBase controller, HttpResponseMessage httpResponseMessage, bool redirectToPreviousPage = true, string defaultUrlRedirect = "/Home")
        {
            string response = null;

            try
            {
                response = httpResponseMessage.Content.ReadAsStringAsync().Result;

                response = JsonConvert.DeserializeObject<string>(response);
            }
            catch (System.Exception)
            {
                throw;
            }

            return ShowMessage(controller, response, httpResponseMessage.StatusCode, redirectToPreviousPage, defaultUrlRedirect);
        }

        public static ActionResult ShowInvalidModelMessages(this ControllerBase controller, bool redirectToPreviousPage = true, string defaultUrlRedirect = "/Home")
        {
            if (controller.ModelState.IsValid)
                throw new InvalidOperationException("Assure ModelState is invalid before calling this method");

            var errors = controller.ModelState.Keys
                .SelectMany(key => controller.ModelState[key].Errors.Select(x => $"{key}: {x.ErrorMessage}"));

            return ShowMessage(controller, string.Join(", ", errors), HttpStatusCode.BadRequest, redirectToPreviousPage, defaultUrlRedirect);
        }

        public static ActionResult ShowMessage(this ControllerBase controller, string message, HttpStatusCode httpStatusCode, bool redirectToPreviousPage = true, string defaultUrlRedirect = "/Home")
        {
            ToastrDialogType dialogType;
            var statusCode = (int)httpStatusCode;

            if (statusCode >= 400)
            {
                controller.ModelState.AddModelError(string.Empty, message);
                switch (statusCode)
                {
                    case 400:
                        dialogType = ToastrDialogType.Warning;
                        break;
                    case 404:
                        dialogType = ToastrDialogType.Warning;
                        break;
                    case 409:
                        dialogType = ToastrDialogType.Warning;
                        break;
                    case 422:
                        dialogType = ToastrDialogType.Warning;
                        break;
                    default:
                        if (statusCode >= 500)
                            dialogType = ToastrDialogType.Error;
                        else
                            dialogType = ToastrDialogType.Warning;

                        // Faz o log das exceptions não tratadas
                        ILogger logger = controller.HttpContext.RequestServices.GetService(typeof(ILogger)) as ILogger;

                        if (logger != null)
                            logger.LogError(message);

                        break;
                }
            }
            else
            {
                if (statusCode < 200)
                    dialogType = ToastrDialogType.Info;
                else
                    dialogType = ToastrDialogType.Success;
            }

            var errorMessage = Newtonsoft.Json.JsonConvert.SerializeObject(new { Title = string.Empty, Content = message, @Type = (int)dialogType });
            controller.Response.Cookies.Append(_defaultCookieName, errorMessage);

            if (redirectToPreviousPage)
            {
                string refererAux = controller.Request.Headers["Referer"];

                if (string.IsNullOrEmpty(refererAux) || refererAux.IndexOf(':') == -1)
                    refererAux = $"{controller.Request.Scheme}://{controller.Request.Host}{controller.Request.PathBase}{defaultUrlRedirect}";

                //if (refererAux.IndexOf(':') == -1)
                //{
                //	var request = controller.Request;
                //	var url = string.Concat(request.Scheme,
                //		"://",
                //		request.Host.ToUriComponent());

                //	refererAux = url + refererAux;
                //}
                return controller.Redirect(refererAux);
            }
            else
            {
                return new ContentResult() { StatusCode = (int)httpStatusCode, Content = message };
            }
        }

    }
}

