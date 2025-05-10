# PowerTrade Watchlist Indicator (Educational Example)

This repository contains a **didactic implementation** of a custom Quantower indicator that demonstrates a possible use case of the [`PowerTradesExamples`](https://github.com/Quantower/Examples/blob/master/Common/PowerTradesExamples.cs) aggregation logic provided by the Quantower team.

> ⚠️ **Disclaimer:**  
> This project is intended for educational and prototyping purposes only. It is **not optimized for production use**.

## 🔍 Purpose

The goal of this implementation is to illustrate how to:
- Leverage the `HistoryAggregationPowerTrades` to aggregate Power Trades.
- Integrate the logic into a watchlist-compatible indicator (`IWatchlistIndicator`).
- React to real-time Power Trade events via `PowerTradesEvent`.

## 🧠 What's inside?

The project consists of two main components:
- **`PowerTradesExamples.cs`** – A helper class that encapsulates the logic to fetch and subscribe to Power Trades aggregation.
- **`PowerTradeWatchlistIndicator.cs`** – A sample Quantower indicator that initializes the Power Trades logic and logs events.

## ✅ Key Notes

- The `PowerTradesExamples` constructor allows configuring the `HistoryAggregationPowerTradesParameters` such as:
  - `TotalVolume`, `DeltaFilter`, `Min/MaxZoneHeight`, `BasisVolumeInterval`, etc.
- The `Initialize()` method fetches historical data and subscribes to real-time updates.
- A `CancellationTokenSource` is used for safe termination, but **asynchronous management is strongly recommended** in real-world applications to avoid UI or performance issues.

## 🛠 Suggested Improvements

For a more robust and production-ready implementation, consider:
- Managing `PowerTradesExamples` asynchronously using `async/await` patterns.
- Proper disposal and unsubscription of events.
- Handling reconnection scenarios or symbol updates.
- External parameterization of Power Trade filters.

## 📎 References

- [Quantower Power Trades Help Page](https://help.quantower.com/quantower/analytics-panels/chart/power-trades)
- [Quantower GitHub Examples](https://github.com/Quantower/Examples)

## 📄 License

All rights of the original API and base examples belong to **Quantower LLC**. This adaptation is shared **for learning purposes only**.
