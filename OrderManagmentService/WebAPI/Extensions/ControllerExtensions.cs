using Microsoft.AspNetCore.Mvc;
using WebAPI.Utils;

namespace WebAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static string GetCurrentUsername(this ControllerBase controller)
        {
            return HttpContextUtil.GetCurrentUsername(controller.HttpContext);
        }

        public static MongoDB.Bson.ObjectId GetClientFlowId(this ControllerBase controller)
        {
            return (MongoDB.Bson.ObjectId)controller.HttpContext.Items["ClientFlowId"];
        }
    }
}
