using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using KPWebApp.Domain.Abstract;

namespace KPWebApp.Domain.Entities
{
    public class Post
    {
        [Key]
        [HiddenInput]
        [DisplayName("ID статті")]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Please enter a post header.")]
        [DisplayName("Заголовок")]
        public string Header { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a post body.")]
        [DisplayName("Текст статті")]
        public String Text { get; set; }

        [HiddenInput]
        [DisplayName("Час створення статті")]
        public DateTime Time { get; set; }

        [UIHint("Enum")]
        [Required(ErrorMessage = "Please specify a category (News/History/InterestingFacts).")]
        [DisplayName("Категорія")]
        public PostCategory Category { get; set; }

        [DisplayName("Світлина")]
        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        public Post()
        {
            this.Time = DateTime.Now;
        }

        public virtual User User { get; set; }

    }

    public enum PostCategory
    {
        News, History, InterestingFacts
    }

}
