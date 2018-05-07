using App11.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App11.chatemplate
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate FromTemplate { get; set; }
        public DataTemplate ToTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return ((ChatModel)item).user_type.ToUpper().Equals("SENDER") ? FromTemplate : ToTemplate;

        }
    }
}
