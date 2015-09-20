using System.Runtime.Serialization;

namespace AuthorService.Interfaces
{
    [DataContract]
    public class AuthorModel
    {
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Twitter { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
