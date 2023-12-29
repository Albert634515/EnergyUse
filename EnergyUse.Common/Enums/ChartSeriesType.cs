using System.ComponentModel;

namespace EnergyUse.Common.Enums
{
    public enum ChartSeriesType
    {
        [Description("Gross value")]
        GrossValue,
        [Description("Low")]
        Low,
        [Description("Avg Low")]
        AvgLow,
        [Description("Avg value low")]
        AvgValueLow,
        [Description("Low predicted")]
        LowPredicted,
        [Description("Return Low predicted")]
        ReturnLowPredicted,
        [Description("Normal")]
        Normal,
        [Description("Avg Normal")]
        AvgNormal,
        [Description("Avg value normal")]
        AvgValueNormal,
        [Description("Normal Predicted")]
        NormalPredicted,
        [Description("Return Normal predicted")]
        ReturnNormalPredicted,
        [Description("Low return delivery")]
        ReturnLow,
        [Description("Avg Low return delivery")]
        ReturnAvgLowDelivery,
        [Description("Normal return delivery")]
        ReturnNormal,
        [Description("Avg Normal return delivery")]
        ReturnAvgNormalDelivery,
        [Description("Consumed")]
        Consumed,
        [Description("Consumed predicted")]
        ConsumedPredicted,
        [Description("Produced")]
        Produced,
        [Description("Produced predicted")]
        ProducedPredicted,
        [Description("Total")]
        Total,
        [Description("Total predicted")]
        TotalPredicted,
        [Description("Unknown")]
        Unknown
    }
}
