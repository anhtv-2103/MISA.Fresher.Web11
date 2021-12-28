using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB11.Core.Exceptions
{
    public class MISAValidateException:Exception
    {
        IDictionary MISAData = new Dictionary<string, object>();
        public MISAValidateException(object data)
        {
            MISAData.Add("data", data);
        }
        public override string Message
        {
            get
            {
                return "Dữ liệu không hợp lệ";
            }
        }
        public override IDictionary Data
        {
            get 
            { 
                return MISAData; 
            }
        }
    }
}
