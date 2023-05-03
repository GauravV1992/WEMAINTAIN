using Repositories.Common;
using System.ComponentModel.DataAnnotations;

namespace Repositories
{
   public class Banner : BaseEntity
    {
        public int Id { get; set; }
        public int Rank  { get; set; }
        public string Ext { get; set; }
    }
}
