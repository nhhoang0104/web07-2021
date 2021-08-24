﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CukCuk.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MISARequired : Attribute
    {
        public readonly string Message;
        public MISARequired()
        {
           
        }
    }

    public class MISAExport : Attribute
    {
        public readonly bool isHidden;
        public MISAExport(bool hidden)
        {
            this.isHidden = !hidden;
        }
    }
}
