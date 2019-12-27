# IoT-Dashboard-Functions
Backend Azure Function for retrieving exchange rate, stock price, and weather information used in [iot-dashboard](https://github.com/JeffreyCA/iot-dashboard).

Forex and stock data from [twelvedata](https://twelvedata.com/), weather data from [OpenWeatherMap](https://openweathermap.org/).

## Requirements
* Active Azure subscription to deploy to Azure Functions

## Running Locally
1. `git clone` this repo
2. Fill in twelvedata and OpenWeatherMap API keys in `local.settings.json`
3. [Code and test Azure Function locally](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-local)

## Deploying to Azure
1. [Publish to Azure](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=nodejs#publish-to-azure)
2. Upload API keys to Application Settings
