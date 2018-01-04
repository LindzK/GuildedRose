# Guided Rose Attempt

My attempt at the guilded rose project

## Getting Started

This is a visual studio 2017 solution, which consists of a library performing the rules and calculations of data, an MS Test project consisting of the projects unit tests and a console application for easy running.
In addition to running the tests, which test the programs specified input and output - you are able to run a console application which has been added for ease of playing with the class library.
If you build the visual studio solution, you can then run the console application directly from visual studio and you will be presented with a list of commands and actions that you can do.

### Prerequisites

No installs are needed for this, but you will need visual studio 2017 with the test libraries to be able to run it.


## Running the tests

The tests are written as an MS Test Project.  To run these you can simply use the Run All tests button from the test explorer within visual studio

### Test Explanation

**RetrieveUpdatedStockAfter1Day** Tests the expected input and output as per the specification.

The following tests explicitly test the rules as specified in the guided rose spec provided to me :

**agedBrieIncreasesInQualityWithAge** Ensures rule Aged Brie actually increases in Quality the older it gets.
**backStagePassIncreasesInQualityWithAge** Ensures rule "Backstage passes", like aged brie, increases in Quality as its SellIn value
approaches;
**backStagePassesDropToZeroAfterConcert** Ensures rule "Backstage passes" quality drops to 0 after the concert.
**backStagePassesIncreaseBy2With10DaysOrLess** Ensures rule "Backstage passes" quality increases by 2 when there are 10 days or less.
**backStagePassesIncreaseBy3With5DaysOrLess** Ensures rule "Backstage passes"  quality increases by 3 when there are 5 days or less.
**conjuredItemsdegradeInQualityTwiceAsFastAsNormalItems** Ensures rule "Conjured" items degrade in Quality twice as fast as normal items.
**itemWithNegativeQualityIsSetToZero** Ensures rule The Quality of an item is never negative.

The following tests are for items that I have implemented to enable this solution to work :

**ItemFactoryReturnsCorrectType** I created a factory which returns an object that implements the IStockable interface to guarantee all objects had some required functions, this rule tests that the factory is returning the correct instance of an object for the correct parameters passed in.


