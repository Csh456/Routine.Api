using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarTest.Entities
{
    ///<summary>
    ///
    ///</summary>
    public class studentinfo
    {
           public studentinfo(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 
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
