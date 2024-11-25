using System.ComponentModel.DataAnnotations;

namespace FaceMan.EntityFrameworkCore
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
