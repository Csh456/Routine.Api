using System;
using System.Linq;
using System.Text;

namespace SqlSugarTest.Entities
{
    ///<summary>
    ///
    ///</summary>
    public partial class studentinfo
    {
           public studentinfo(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Age {get;set;}

    }
}
