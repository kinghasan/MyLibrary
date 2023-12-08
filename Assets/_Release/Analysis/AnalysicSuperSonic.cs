#if SuperSonic
using Aya.Extension;
using SupersonicWisdomSDK;

namespace Aya.Analysis
{
    public class AnalysicSuperSonic : AnalysisBase
    {
        public override void LevelStart(string level)
        {
            SupersonicWisdom.Api.NotifyLevelStarted(level.AsLong(), null);
        }

        public override void LevelCompleted(string level)
        {
            SupersonicWisdom.Api.NotifyLevelCompleted(level.AsLong(), null);
        }

        public override void LevelFailed(string level)
        {
            SupersonicWisdom.Api.NotifyLevelFailed(level.AsLong(), null);
        }
    }
}
#endif