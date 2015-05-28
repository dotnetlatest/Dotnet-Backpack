m## Moq - CheatSheet 

Moq is a popular & friendly mocking framework for .NET. The API  follows the **arrange-act-assert style** and relies heavily on .NET 3.5 features, such as lambdas and extension methods. 

----------

###Terms

- **Mocked Type** - is a interface/class we intend to test by creating a fake version of. 
eg. 

` 
Mock productRepoistory= new Mock<IProductRepository>()
`
 
In this example `IProductRepository` is the mocked Type.
-  **Mock** - is the object that is created when we are given a instance of a mocked type. In the above example our mock is `productRepository`.

### When to create Mocks?
> 
Mocks are most useful when used to create objects that you might not have access to in the context of a unit test. For example, database access, external services


### Common workflow


#### Arrange
> mock an object / dependency
> 
> setup test conditions
 



#### Act
> Perform test



#### Assert
> Verify the test 

----------

###Commonly Used Methods

- `mock.setup()`: 

This method allows you to define conditions and expected results for the test using our mock object

 
- `mock.verify()` :

This method will determine if the conditions created using `mock.setup` have been met

- `mock.verifyAll()`

This method will determine if all the conditions created using have been met

- `mock.setup()` 


This method can be used to check if a property has been set to a value.

- `mock.SetupSet()` 


This method specifics that all properties on the mock should have property behaviour.The default value will be determine by `Moq.Mock.DefaultValue`

- `mock.SetupAllProperties()`


----------


###Examples

####mock.setup()

> This test setups the mock object such that when the method Hello is called it returns true.

```c#

	var mock = new Mock<TestClass>();
    mock.Setup(x => x.Hello()).Returns(true);
    Assert.AreEqual(mock.Object.Hello(), true);
			
```
> This test setups the mock object to return null

```c#

	
	//Arrange
    var customerToCreateDto = new CustomerToCreateDto 
                            {FirstName = "Bob", LastName = "Builder"};
    var mockAddressBuilder = new Mock<ICustomerAddressBuilder>();
    var mockCustomerRepository = new Mock<ICustomerRepository>();

	mockAddressBuilder
           .Setup(x => x.From(It.IsAny<CustomerToCreateDto>()))
           .Returns(() => null);

    var customerService = new CustomerService(
           mockAddressBuilder.Object, 
           mockCustomerRepository.Object);
                
    //Act
    customerService.Create(customerToCreateDto);

	//Assert
			
```
https://stackoverflow.com/questions/7564038/how-are-integration-tests-written-for-interacting-with-external-api
