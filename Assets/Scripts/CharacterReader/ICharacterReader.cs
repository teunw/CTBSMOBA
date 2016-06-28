using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.CharacterReader
{
    interface ICharacterReader
    {

        bool GetMembers(ref List<MemberData> members);

    }
}
