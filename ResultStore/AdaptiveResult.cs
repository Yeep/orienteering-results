using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ResultStore
{
    public class AdaptiveResult : ViewResult
    {
        private object m_result;
        private readonly IEnumerable<JavaScriptConverter> m_converters;

        public AdaptiveResult(object result) {
            m_result = result;
            //m_converters = ServiceLocator.Current.GetAllInstances<JavaScriptConverter>();
        }

        public AdaptiveResult(object result, IEnumerable<JavaScriptConverter> converters) {
            m_result = result;
            m_converters = converters;
        }

        public override void ExecuteResult(ControllerContext context) {
            ExecuteAjaxView(context);
        }

        private void ExecuteAjaxView(ControllerContext context) {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            json_serializer.RegisterConverters(m_converters);
        }
    }
}