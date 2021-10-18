sledilnikCovid assignment
WebAPI assignment no.2 for an internship at IndigoLabs

## Filters (only applicable to app/region/cases)

* region - **string** (Filters cases based on entered region): e.g. `region=lj`
* from - **DateTime** (Filters cases from this date): e.g `from=2020-01-01`
* to - **DateTime** (Filters cases to this date): e.g `from=2021-05-05`

## Endpoints

* api/region/cases:

It supports **optional** query parameters such as Region, To and From. Upon the GET request it exposes 4 types of data properties: DailyActiveCases, DeceasedToDate, FirstVaccinatedToDate, SecondVaccinatedToDate. Resultset is written in default JSON format. It supports optional query parameters.

* api/region/lastweek:

It exposes the sum of number of active cases in the past 7 days for each available region, including foreign and unknown. The data set is available in the descending order, ordered by the latter sum. Resultset is written in JSON
format. It does not support additional query parameters.

* API Authentication (BasicAuth):

Username: guide <br/>
Password: senk

I enabled authentication upon first interaction with the site rather than the specific endpoint (be that \cases or \lastweek).
