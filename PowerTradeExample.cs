using System;
using System.Threading;
using TradingPlatform.BusinessLayer;
using TradingPlatform.BusinessLayer.History.Aggregations;
using TradingPlatform.BusinessLayer.PowerTrades;

namespace PowerTradeWatchlistIndicator
{
    internal class PowerTradesExamples
    {
        /// <summary>
        /// https://help.quantower.com/quantower/analytics-panels/chart/power-trades
        /// </summary>
        public Symbol symbol { get; set; }

        public event EventHandler<IHistoryItem> PowerTradesEvent;
        public HistoricalData PowerTradesHistoricalData { get; set; }

        private CancellationTokenSource cts;
        private DateTime from;
        private DateTime to;
        private HistoryAggregationPowerTradesParameters parameters;

        public PowerTradesExamples(Symbol symbol, DateTime from, DateTime to = default,  int ctsTimespan = 30, HistoryAggregationPowerTradesParameters parameters = null)
        {
            this.symbol = symbol;
            this.cts = new CancellationTokenSource(TimeSpan.FromSeconds(ctsTimespan));
            this.parameters = parameters != null? parameters :
                new HistoryAggregationPowerTradesParameters()
                {
                    TotalVolume = 100,
                    MinTradeVolume = 0,
                    MaxTradeVolume = 100000,
                    TimeInterval = 5,
                    BasisVolumeInterval = 300,
                    MinZoneHeight = 0,
                    MaxZoneHeight = 100000,
                    DeltaFilter = 50,
                    BasisRatioFilter = 0,
                };

        }

        public void Initialize()
        {
            try
            {
                this.PowerTradesHistoricalData = this.symbol.GetHistory(new HistoryRequestParameters()
                {
                    Aggregation = new HistoryAggregationPowerTrades(this.parameters),
                    FromTime = this.from,
                    ToTime = this.to,
                    Symbol = this.symbol,
                    CancellationToken = this.cts.Token,
                });

                // process historical 'PowerTrades' items
                this.ProcessHistorical();

                // subscribe to real-time updates (we want to receive new items)
                PowerTradesHistoricalData.NewHistoryItem += this.HistoricalData_NewHistoryItem;
            }
            catch (Exception ex)
            {
                Core.Instance.Loggers.Log(ex.Message, LoggingLevel.Error);
            }
            

        }

        private void ProcessHistorical()
        {
            // process historical 'PowerTrades' items
            for (int i = 0; i < PowerTradesHistoricalData.Count; i++)
            {
                var historyItem = PowerTradesHistoricalData[i, SeekOriginHistory.Begin];
                this.PowerTradesEvent?.Invoke(this, historyItem);
            }
            
        }

        /// <summary>
        /// 'NewHistoryItem' handler
        /// </summary>
        private void HistoricalData_NewHistoryItem(object sender, HistoryEventArgs e)
        {
            if (e.HistoryItem is not IPowerTradesHistoryItem powerTradesItem)
                return;
            this.PowerTradesEvent?.Invoke(this, e.HistoryItem);
        }
    }
}
