Feature: From Birth Till Death

    Scenario: Adding a cow
        Given the cow
          | Fields             | Value               |
          | LifeNumber         | NL1234              |
          | Gender             | Female              |
          | DateOfBirth        | 2012-03-05T12:00:00 |
          | DateCalved         | 2016-02-14T12:00:00 |
          | DateOfDeath        | 2021-01-01T12:00:00 |
          | LifeNumberOfMother |                     |
        And with the following events
          | LocationNumber | OccuredAt           | Reason  |
          | 00000001       | 2012-03-05T12:00:00 | Born    |
          | 00000001       | 2015-09-02T12:00:00 | Sold    |
          | 00000002       | 2015-09-02T12:00:00 | Arrived |
          | 00000002       | 2015-10-02T12:00:00 | Sold    |
          | 00000003       | 2015-10-02T12:00:00 | Arrived |
          | 00000003       | 2016-02-14T12:00:00 | Calved  |
          | 00000003       | 2021-01-01T12:00:00 | Died    |
        When sending it to the API
        Then the cow data should be accepted
        And the animal history should look like this
          | Reason     | OccuredAt           | Category |
          | Born       | 2012-03-05T12:00:00 | 10       |
          | Transition | 2015-09-02T12:00:00 | 10       |
          | Sold       | 2015-09-02T12:00:00 | 10       |
          | Arrived    |                     | 10       |