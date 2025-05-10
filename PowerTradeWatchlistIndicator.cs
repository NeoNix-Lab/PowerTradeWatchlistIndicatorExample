// Copyright QUANTOWER LLC. © 2017-2023. All rights reserved.

using System;
using System.Drawing;
using TradingPlatform.BusinessLayer;

namespace PowerTradeWatchlistIndicator
{
	/// <summary>
	/// An example of blank indicator. Add your code, compile it and use on the charts in the assigned trading terminal.
	/// Information about API you can find here: http://api.quantower.com
	/// Code samples: https://github.com/Quantower/Examples
	/// </summary>
	public class PowerTradeWatchlistIndicator : Indicator , IWatchlistIndicator
	{
		/// <summary>
		/// Indicator's constructor. Contains general information: name, description, LineSeries etc. 
		/// </summary>
		/// 
		private bool isPowertradeInitilized = false;
		private PowerTradesExamples powerTradesExamples;
		public PowerTradeWatchlistIndicator()
			: base()
		{
			// Defines indicator's name and description.
			Name = "PowerTradeWatchlistIndicator";
			Description = "My indicator's annotation";

			// Defines line on demand with particular parameters.
			AddLineSeries("line1", Color.CadetBlue, 1, LineStyle.Solid);

			// By default indicator will be applied on main window of the chart
			SeparateWindow = false;
		}

		public int MinHistoryDepths => 10;

		/// <summary>
		/// This function will be called after creating an indicator as well as after its input params reset or chart (symbol or timeframe) updates.
		/// </summary>
		protected override void OnInit()
		{
			// Add your initialization code here
		}

		/// <summary>
		/// Calculation entry point. This function is called when a price data updates. 
		/// Will be runing under the HistoricalBar mode during history loading. 
		/// Under NewTick during realtime. 
		/// Under NewBar if start of the new bar is required.
		/// </summary>
		/// <param name="args">Provides data of updating reason and incoming price.</param>
		protected override void OnUpdate(UpdateArgs args)
		{
			if (!isPowertradeInitilized)
            {
                powerTradesExamples = new PowerTradesExamples(this.Symbol, this.HistoricalData.FromTime, this.HistoricalData.ToTime);
                powerTradesExamples.Initialize();
                powerTradesExamples.PowerTradesEvent += this.PowerTradesExamples_PowerTradesEvent;
                isPowertradeInitilized = true;
            }
		}

		private void PowerTradesExamples_PowerTradesEvent(object sender, IHistoryItem e)
		{
			Core.Instance.Loggers.Log($"PowerTradeWatchlistIndicator: {nameof(this.powerTradesExamples)} justRaisedEvent with Item:  {e}", LoggingLevel.Trading);
			//do whatever you want with the event
		}
    }
}
