using System.Runtime.Serialization;
using SuperMemo.DomainModel;

namespace DTO
{
    [DataContract]
    public class CardDto
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Word { get; set; }
        [DataMember]
        public string Translation { get; set; }

        public CardDto(Card card)
        {
            ID = card.Id;
            Word = card.Word;
            Translation = card.Translation;
        }

        public CardDto()
        {
        }
    }
}
