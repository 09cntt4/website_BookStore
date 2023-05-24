using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// 2 thu viên thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BookStore.Models
{
    [MetadataTypeAttribute(typeof(SACHmeteadata))]
    public partial  class SACH
    {

        internal sealed class SACHmeteadata 
        {

        }


    }
}