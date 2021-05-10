using Aptacode.Expressions.Json;
using Aptacode.StateNet.Json;
using Aptacode.StateNet.Network;
using Newtonsoft.Json;
using StateNet.Tests.Network.Helpers;
using Xunit;

namespace StateNet.Tests.Json
{
    public class Json_Tests
    {
        [Fact]
        public void StateNetSerialisationTest()
        {
            //Arrange
            var network = StateNetwork_Helpers.State_WithMultiple_Inputs_Network;

            //Act
            var expressionSubtypes = new ExpressionsSubTypes().AddStateNet();
            var settings = new JsonSerializerSettings().Add(expressionSubtypes);

            var json = JsonConvert.SerializeObject(network, settings);

            var result = JsonConvert.DeserializeObject<StateNetwork>(json, settings);


            //Assert
            Assert.True(network == result);
        }
    }
}