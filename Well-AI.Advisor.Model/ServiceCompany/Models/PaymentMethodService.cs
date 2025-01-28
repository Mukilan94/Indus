using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using WebAI.Advisor.Helper;
using WebAI.Advisor.Model.ServiceCompany.Models;

namespace WebAI.Advisor.Model.ServiceCompany.Models
{
    public class PaymentMethodService
    {
        private ISession _session;

        public ISession Session { get { return _session; } }

        public PaymentMethodService()
        {
            _session = WellAIAppContext.Current.Session;
        }

        public void Create(PaymentMethod paymeth)
        {
            var methods = GetAll().ToList();
            var first = methods.OrderByDescending(e => e.PayMethodID).FirstOrDefault();
            var id = (first != null) ? first.PayMethodID : 0;

            paymeth.PayMethodID = id + 1;

            methods.Add(paymeth);

            Session.SetObjectAsJson("PaymentMethods", methods);
        }

        public List<PaymentMethod> GetAll()
        {
            var result = Session.GetObjectFromJson<List<PaymentMethod>>("PaymentMethods");

            if (result == null)
            {
                result = new List<PaymentMethod>
                {
                    new PaymentMethod{ PayMethodID=1, System="Visa",Holder = "Bob", Number = "123456789012", Expire=new System.DateTime(2022,10,2), Default=false },
                    new PaymentMethod{ PayMethodID=2, System="Mastercard",Holder = "Jack", Number = "123423419012", Expire=new System.DateTime(2024,10,2), Default=true },
                };

                Session.SetObjectAsJson("PaymentMethods", result);

            }

            return result;
        }

        public IEnumerable<PaymentMethod> Read()
        {
            return GetAll();
        }

        public void Update(PaymentMethod method)
        {
            var methods = GetAll();
            var target = methods.FirstOrDefault(e => e.PayMethodID == method.PayMethodID);

            if (target != null)
            {
                target.Holder = method.Holder;
                target.Number = method.Number;
                target.Default = method.Default;
                if(method.Expire.HasValue)
                    target.Expire = method.Expire.Value;
                target.System = method.System;
            }

            Session.SetObjectAsJson("PaymentMethods", methods);
        }
    }
}
