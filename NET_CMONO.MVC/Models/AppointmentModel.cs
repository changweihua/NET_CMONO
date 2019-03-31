using System;
using Microsoft.AspNetCore.Mvc;
using NET_CMONO.MVC.ModelBinders;

namespace NET_CMONO.MVC.Models
{
    public class AppointmentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [ModelBinder(BinderType = typeof(SplitDateTimeModelBinder))]
        public DateTime AppointmentDate { get; set; }
    }
}