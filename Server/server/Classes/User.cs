using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using post;

namespace user {
    public class User 
    {
        public string name;
        
        [DataType(DataType.EmailAddress)]
        public EmailAddressAttribute email;

        public string institution;

        public string degree;

        public Collection<Post> collaborating_posts;
    }
}    