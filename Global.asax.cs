using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pasticceria
{
        
    public class MvcApplication : System.Web.HttpApplication
    {
        private Model2 db = new Model2();
        protected void Application_Start()
        {

            //Per aggiornare i prezzi ogni 24 ore usando un timer:
          
            #region Timer
            /*
             System.Timers.Timer DayTimer = new System.Timers.Timer();
            DayTimer = new System.Timers.Timer(10000);
            DayTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            DayTimer.Start();
            void OnTimedEvent(object source, ElapsedEventArgs e)
            {
                //Do the stuff you want to be done every hour;

                foreach (var i in db.Product.ToList())
                {
                    if (DateTime.Today.Hour == i.ProdDate.Value.Hour + 24)
                    {
                        i.Price = i.Price * (80 / 100);
                    }
                    else if (DateTime.Today.Day == i.ProdDate.Value.Day + 2)
                    {
                        i.Price = i.Price * (20 / 100);
                    }
                    else if (DateTime.Today.Day == i.ProdDate.Value.Day + 3)
                    {
                        db.Product.Remove(i);
                        db.SaveChanges();
                    }
                }
            }*/
            #endregion

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    
    }
}
