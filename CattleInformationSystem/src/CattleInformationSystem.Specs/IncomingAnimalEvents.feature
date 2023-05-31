Feature: Incoming Animal Events
Animal events, coming from the Netherlands Enterprise Agency (RVO) should be processed into the database.

    Background: Creating a farm
        Given a farm of type 'BreedingForMilk', with UBN '20000000001'
        And a farm of type 'BreedingForMeat', with UBN '20000000002'
        And a farm of type 'Milk', with UBN '20000000003'
        And a farm of type 'Milk', with UBN '20000000004'
        And a farm of type 'Milk', with UBN '20000000005'
        And a farm of type 'Meat', with UBN '20000000006'

    Scenario: Female, Being born
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000001 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        When it is added to the incoming events table
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |

    Scenario: Female, sold and bought to a milk farm
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000002 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2017-03-25', from UBN '20000000001' to UBN '20000000003'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-03-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2017-03-25 | 101      |

    Scenario: Female, sold and bought to a meat farm
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000003 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2017-03-25', from UBN '20000000001' to UBN '20000000006'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-03-25 | 101      |
          | 20000000006 | Arrival   | 0     | 2017-03-25 | 101      |

    Scenario: Female, sold and bought to 3 farms
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000004 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2017-03-25', from UBN '20000000001' to UBN '20000000003'
        And it is moved on '2017-05-01', from UBN '20000000003' to UBN '20000000004'
        And it is moved on '2017-08-27', from UBN '20000000004' to UBN '20000000005'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-03-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2017-03-25 | 101      |
          | 20000000003 | Departure | 0     | 2017-05-01 | 101      |
          | 20000000004 | Arrival   | 0     | 2017-05-01 | 101      |
          | 20000000004 | Departure | 0     | 2017-08-27 | 101      |
          | 20000000005 | Arrival   | 0     | 2017-08-27 | 101      |

    Scenario: Female, sold and bought to 1 farm, then dying
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000005 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2017-03-25', from UBN '20000000001' to UBN '20000000003'
        And it died on '2017-04-05' on UBN '20000000003'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-03-23 | 101      |
          | 20000000001 | Departure | 0     | 2017-03-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2017-03-25 | 101      |
          | 20000000003 | Death     | 0     | 2017-04-05 | 101      |
        And the cow should have a date of death of '2017-04-05'
        And the end date on the latest location should be set to '2017-04-05'

    Scenario: Female, sold and bought, gave birth
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000006 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-05-01  |
        And it is added to the incoming events table
        When it is moved on '2018-07-25', from UBN '20000000001' to UBN '20000000003'
        And it gave birth on '2019-04-03' on UBN '20000000003'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-05-01 | 101      |
          | 20000000001 | Departure | 0     | 2018-07-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2018-07-25 | 102      |
          | 20000000003 | Calved    | 0     | 2019-04-03 | 100      |

    Scenario: Female, sold and bought, gave birth on arrival, died the same day
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000006 |
          | Gender      | Female      |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-05-01  |
        And it is added to the incoming events table
        When it is moved on '2019-07-25', from UBN '20000000001' to UBN '20000000003'
        And it gave birth on '2019-07-25' on UBN '20000000003'
        And it died on '2019-07-25' on UBN '20000000003'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000001 | Birth     | 0     | 2017-05-01 | 101      |
          | 20000000001 | Departure | 0     | 2019-07-25 | 101      |
          | 20000000003 | Arrival   | 0     | 2019-07-25 | 102      |
          | 20000000003 | Calved    | 1     | 2019-07-25 | 100      |
          | 20000000003 | Death     | 2     | 2019-07-25 | 100      |

    Scenario: Male, Being born
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000007 |
          | Gender      | Male        |
          | Ubn         | 20000000001 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        When it is added to the incoming events table
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason | Order | Date       | Category |
          | 20000000001 | Birth  | 0     | 2017-03-23 | 101      |

    Scenario: Male, being born on a meat breeding farm, moved to different farms
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000008 |
          | Gender      | Male        |
          | Ubn         | 20000000002 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2018-07-25', from UBN '20000000002' to UBN '20000000006'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000002 | Birth     | 0     | 2017-03-23 | 0        |
          | 20000000002 | Departure | 0     | 2018-07-25 | 0        |
          | 20000000006 | Arrival   | 0     | 2018-07-25 | 0        |

    Scenario: Male, being born on a meat breeding farm, moved to a milk breeding farm
        Given an animal, born today
          | Key         | Value       |
          | Life Number | 10000000008 |
          | Gender      | Male        |
          | Ubn         | 20000000002 |
          | Reason      | Birth       |
          | EventDate   | 2017-03-23  |
        And it is added to the incoming events table
        When it is moved on '2018-07-25', from UBN '20000000002' to UBN '20000000001'
        Then it should be processed and stored in the database
        And have the events(s)
          | Farm        | Reason    | Order | Date       | Category |
          | 20000000002 | Birth     | 0     | 2017-03-23 | 0        |
          | 20000000002 | Departure | 0     | 2018-07-25 | 0        |
          | 20000000001 | Arrival   | 0     | 2018-07-25 | 101      |