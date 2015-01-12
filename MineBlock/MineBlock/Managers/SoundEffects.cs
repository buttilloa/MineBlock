using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineBlock
{
    public class SoundEffects
    {
        public static SoundEffectInstance RickRoll;
        public static SoundEffectInstance ChestOpen;
        public static SoundEffectInstance Rain;
        public static SoundEffectInstance Snow;
        public static void LoadSounds(ContentManager Content)
        {
            RickRoll = Content.Load<SoundEffect>(@"Sounds/RickRoll").CreateInstance();
            ChestOpen = Content.Load<SoundEffect>(@"Sounds/Chest").CreateInstance();
            Rain = Content.Load<SoundEffect>(@"Sounds/Rain").CreateInstance();
            Snow = Content.Load<SoundEffect>(@"Sounds/Snow").CreateInstance();
            
        }

    }
}
