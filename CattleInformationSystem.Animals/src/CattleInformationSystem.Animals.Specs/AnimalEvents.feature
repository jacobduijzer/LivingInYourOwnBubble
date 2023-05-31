Feature: Animal Events
Fake events to test the animal processing. The life number will be generated!

    Scenario: A newborn female on a milk breeding farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database

    Scenario: A newborn female on a milk farm, dying the next day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-24 |
        When added to the queue
        Then it will be processed and added to the legacy database
        
    Scenario: A newborn female on a milk farm, dying the same day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        
    Scenario: A newborn female on a milk breeding farm, giving birth after 3 years
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Calved | 20000000001 |           | 2020-02-20 |
        When added to the queue
        Then it will be processed and added to the legacy database

    Scenario: A newborn male on a milk farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Male   | 2017-03-23  | Birth  | 20000000001 |           | 2023-05-23 |
        When added to the queue
        Then it will be processed and added to the legacy database

    Scenario: The full history of a female cow
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000002 | 2017-06-24 |
          | Female | 2017-03-23  | Arrival   | 20000000005 |             | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000005 | 20000000006 | 2019-04-02 |
          | Female | 2017-03-23  | Arrival   | 20000000006 | 20000000009 | 2019-04-02 |
          | Female | 2017-03-23  | Death     | 20000000009 |             | 2019-04-02 |
        When added to the queue
        Then it will be processed and added to the legacy database