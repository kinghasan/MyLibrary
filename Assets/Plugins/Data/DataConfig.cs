using UnityEngine;

namespace Aya.Data
{
    [CreateAssetMenu(fileName = "DataSetting", menuName = "Setting/Data Setting")]
    public class DataConfig : DataSetting<DataConfig>
    {
        [Tips("≤‚ ‘")]public int TestValue;
    }
}
