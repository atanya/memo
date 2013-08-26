using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SuperMemo.DomainModel;

namespace DTO
{
    [DataContract]
    public class TrainingDto
        {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Word { get; set; }
        [DataMember]
        public string Translation { get; set; }
        [DataMember]
        public long Total { get; set; }
        [DataMember]
        public int WordsForToday { get; set; }

        public TrainingDto(Card card, long total, int wordsLeft)
        {
            ID = card.Id;
            Word = card.Word;
            Translation = card.Translation;
            Total = total;
            WordsForToday = wordsLeft;
        }

        public TrainingDto()
        {
        }
    }

}
