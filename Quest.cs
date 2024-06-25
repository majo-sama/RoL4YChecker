using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoL4YChecker
{
    public class Quest
    {
        public int Number { get; private set; }
        public int Phase { get; private set; }
        public string Name { get; private set; }
        public QuestStatus Status { get; set; } = QuestStatus.Waiting;

        public enum QuestStatus
        {
            Waiting,
            Accepted,
            Cleared
        }

        public override string ToString()
        {
            return $"{Name} - {Status}";
        }
    }
}
