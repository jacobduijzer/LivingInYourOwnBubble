Feature: Incoming Animal Events
Animal events, coming from the Netherlands Enterprise Agency (RVO)
should be processed and inserted or updated in the database.

    Scenario: A newborn female on a milk breeding farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate |
          | 20000000001 | 2017-03-23 |         |

    Scenario: A newborn female on a milk farm, dying the next day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-24 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Death  | 0     | 2017-03-24 | 101      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2017-03-24 |
        And the animal should have a date of death of '2017-03-24'

    Scenario: A newborn female on a milk farm, dying the same day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Death  | 1     | 2017-03-23 | 101      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2017-03-23 |
        And the animal should have a date of death of '2017-03-23'

    Scenario: A newborn female on a milk breeding farm, giving birth after 3 years
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Calved | 20000000001 |           | 2020-02-20 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Calved | 0     | 2020-02-20 | 100      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate |
          | 20000000001 | 2017-03-23 |         |
        And the animal should have a date first calved of '2020-02-20'

    Scenario: A newborn male on a milk farm, different event date
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Male   | 2017-03-23  | Birth  | 20000000001 |           | 2023-05-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate |
          | 20000000001 | 2017-03-23 |         |

    Scenario: A female cow, moving to two farms
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2017-06-24 |
          | 20000000006 | 2017-06-24 | 2019-04-08 |
          | 20000000009 | 2019-04-08 |            |

    Scenario: A female cow, moving to four farms
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000002 | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000002 | 20000000005 | 2019-04-08 |
          | Female | 2017-03-23  | Departure | 20000000005 | 20000000006 | 2020-04-08 |
          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2022-11-12 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
          | 20000000002 | Arrival   | 0     | 2017-06-24 | 101      |
          | 20000000002 | Departure | 0     | 2019-04-08 | 101      |
          | 20000000005 | Arrival   | 0     | 2019-04-08 | 102      |
          | 20000000005 | Departure | 0     | 2020-04-08 | 102      |
          | 20000000006 | Arrival   | 0     | 2020-04-08 | 102      |
          | 20000000006 | Departure | 0     | 2022-11-12 | 102      |
          | 20000000009 | Arrival   | 0     | 2022-11-12 | 0        |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2017-06-24 |
          | 20000000002 | 2017-06-24 | 2019-04-08 |
          | 20000000005 | 2019-04-08 | 2020-04-08 |
          | 20000000006 | 2020-04-08 | 2022-11-12 |
          | 20000000009 | 2022-11-12 |            |

    Scenario: A female cow, moving to two farms, then dying
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
          | Female | 2017-03-23  | Death     | 20000000009 |             | 2019-04-08 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |
          | 20000000009 | Death     | 1     | 2019-04-08 | 0        |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2017-06-24 |
          | 20000000006 | 2017-06-24 | 2019-04-08 |
          | 20000000009 | 2019-04-08 | 2019-04-08 |
        And the animal should have a date of death of '2019-04-08'

    Scenario: Female, sold and bought, gave birth with, a different event date than the actual birth date
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-05-01 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000003 | 2018-07-25 |
          | Female | 2017-03-23  | Calved    | 20000000003 |             | 2019-04-03 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2018-07-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2018-07-25 | 102      |
          | 20000000003 | Calved    | 0     | 2019-04-03 | 100      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2018-07-25 |
          | 20000000003 | 2018-07-25 |            |
        And the animal should have a date first calved of '2019-04-03'

    Scenario: Female, sold and bought to a meat farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000007 | 2018-07-25 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2018-07-25 | 101      |
          | 20000000007 | Arrival   | 0     | 2018-07-25 | 101      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-03-23 | 2018-07-25 |
          | 20000000007 | 2018-07-25 |            |

    Scenario: Female, sold and bought, gave birth on arrival, died the same day
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-05-01  | Birth     | 20000000001 |             | 2017-05-01 |
          | Female | 2017-05-01  | Departure | 20000000001 | 20000000005 | 2019-07-25 |
          | Female | 2017-05-01  | Calved    | 20000000005 |             | 2019-07-25 |
          | Female | 2017-05-01  | Death     | 20000000005 |             | 2019-07-25 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-05-01 | 101      |
          | 20000000001 | Departure | 0     | 2019-07-25 | 101      |
          | 20000000005 | Arrival   | 0     | 2019-07-25 | 102      |
          | 20000000005 | Calved    | 1     | 2019-07-25 | 100      |
          | 20000000005 | Death     | 2     | 2019-07-25 | 100      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000001 | 2017-05-01 | 2019-07-25 |
          | 20000000005 | 2019-07-25 | 2019-07-25 |
        And the animal should have a date first calved of '2019-07-25'
        And the animal should have a date of death of '2019-07-25'

    Scenario: Male, being born on a meat breeding farm, moved to different farms, turning 1 years old
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Male   | 2017-03-23  | Birth     | 20000000003 |             | 2017-03-23 |
          | Male   | 2017-03-23  | Departure | 20000000003 | 20000000007 | 2018-07-25 |
          | Male   | 2017-03-23  | Death     | 20000000007 |             | 2019-07-25 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000003 | Birth     | 0     | 2017-03-23 | 0        |
          | 20000000003 | Departure | 0     | 2018-07-25 | 0        |
          | 20000000007 | Arrival   | 0     | 2018-07-25 | 104      |
          | 20000000007 | Death     | 0     | 2019-07-25 | 104      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000003 | 2017-03-23 | 2018-07-25 |
          | 20000000007 | 2018-07-25 | 2019-07-25 |
        And the animal should have a date of death of '2019-07-25'

    Scenario: Male, being born on a meat breeding farm, moved to a milk breeding farm, moved to another farm, turning 1 years old
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Male   | 2017-03-23  | Birth     | 20000000003 |             | 2017-03-23 |
          | Male   | 2017-03-23  | Departure | 20000000003 | 20000000001 | 2017-05-25 |
          | Male   | 2017-03-23  | Departure | 20000000001 | 20000000005 | 2017-06-25 |
          | Male   | 2017-03-23  | Departure | 20000000005 | 20000000006 | 2018-05-25 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Ubn         | Reason    | Order | Date       | Category |
          | 20000000003 | Birth     | 0     | 2017-03-23 | 0        |
          | 20000000003 | Departure | 0     | 2017-05-25 | 0        |
          | 20000000001 | Arrival   | 0     | 2017-05-25 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-25 | 101      |
          | 20000000005 | Arrival   | 0     | 2017-06-25 | 101      |
          | 20000000005 | Departure | 0     | 2018-05-25 | 101      |
          | 20000000006 | Arrival   | 0     | 2018-05-25 | 104      |
        And have the location(s)
          | Ubn         | StartDate  | EndDate    |
          | 20000000003 | 2017-03-23 | 2017-05-25 |
          | 20000000001 | 2017-05-25 | 2017-06-25 |
          | 20000000005 | 2017-06-25 | 2018-05-25 |
          | 20000000006 | 2018-05-25 |            |
    #Feature: Animal Events
    #Fake events to test the animal processing. The life number will be generated!
    #
    #    Scenario: A newborn female on a milk breeding farm
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
    #          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason | Order | Date       | Category |
    #          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
    #
    #    Scenario: A newborn female on a milk farm, dying the next day
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
    #          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
    #          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-24 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason | Order | Date       | Category |
    #          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
    #          | 20000000001 | Death  | 0     | 2017-03-24 | 101      |
    #
    #    Scenario: A newborn female on a milk farm, dying the same day
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
    #          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
    #          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-23 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason | Order | Date       | Category |
    #          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
    #          | 20000000001 | Death  | 1     | 2017-03-23 | 101      |
    #
    #    Scenario: A newborn female on a milk breeding farm, giving birth after 3 years
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
    #          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
    #          | Female | 2017-03-23  | Calved | 20000000001 |           | 2020-02-20 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason | Order | Date       | Category |
    #          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
    #          | 20000000001 | Calved | 0     | 2020-02-20 | 100      |
    #
    #    Scenario: A newborn male on a milk farm
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
    #          | Male   | 2017-03-23  | Birth  | 20000000001 |           | 2023-05-23 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason | Order | Date       | Category |
    #          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
    #
    #    Scenario: A female cow, moving to two farms
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
    #          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
    #          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
    #          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason    | Order | Date       | Category |
    #          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
    #          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
    #          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
    #          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
    #          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |
    #
    #    Scenario: The full history of a female cow
    #        Given the following event(s)
    #          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
    #          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
    #          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
    #          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
    #          | Female | 2017-03-23  | Death     | 20000000009 |             | 2019-04-08 |
    #        When added to the queue
    #        Then it will be processed and added to the legacy database
    #        And have the events(s)
    #          | Farm        | Reason    | Order | Date       | Category |
    #          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
    #          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
    #          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
    #          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
    #          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |
    #          | 20000000009 | Death     | 1     | 2019-04-08 | 0        |