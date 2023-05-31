Feature: Animal Events
Fake events to test the animal processing. The life number will be generated!

    Scenario: A newborn female on a milk breeding farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |

    Scenario: A newborn female on a milk farm, dying the next day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-24 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Death  | 0     | 2017-03-24 | 101      |

    Scenario: A newborn female on a milk farm, dying the same day
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Death  | 20000000001 |           | 2017-03-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Death  | 1     | 2017-03-23 | 101      |

    Scenario: A newborn female on a milk breeding farm, giving birth after 3 years
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Female | 2017-03-23  | Birth  | 20000000001 |           | 2017-03-23 |
          | Female | 2017-03-23  | Calved | 20000000001 |           | 2020-02-20 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |
          | 20000000001 | Calved | 0     | 2020-02-20 | 100      |

    Scenario: A newborn male on a milk farm
        Given the following event(s)
          | Gender | DateOfBirth | Reason | CurrentUbn  | TargetUbn | EventDate  |
          | Male   | 2017-03-23  | Birth  | 20000000001 |           | 2023-05-23 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |

    Scenario: A female cow, moving to two farms
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |

    Scenario: The full history of a female cow
        Given the following event(s)
          | Gender | DateOfBirth | Reason    | CurrentUbn  | TargetUbn   | EventDate  |
          | Female | 2017-03-23  | Birth     | 20000000001 |             | 2017-03-23 |
          | Female | 2017-03-23  | Departure | 20000000001 | 20000000006 | 2017-06-24 |
          | Female | 2017-03-23  | Departure | 20000000006 | 20000000009 | 2019-04-08 |
          | Female | 2017-03-23  | Death     | 20000000009 |             | 2019-04-08 |
        When added to the queue
        Then it will be processed and added to the legacy database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-06-24 | 101      |
          | 20000000006 | Arrival   | 0     | 2017-06-24 | 101      |
          | 20000000006 | Departure | 0     | 2019-04-08 | 101      |
          | 20000000009 | Arrival   | 0     | 2019-04-08 | 0        |
          | 20000000009 | Death     | 1     | 2019-04-08 | 0        |