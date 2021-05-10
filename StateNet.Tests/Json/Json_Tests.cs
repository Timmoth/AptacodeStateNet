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
            var settings = new JsonSerializerSettings().AddStateNet();

            var json = JsonConvert.SerializeObject(network, settings);

            var result = JsonConvert.DeserializeObject<StateNetwork>(json, settings);


            //Assert
            Assert.True(network == result);
        }
    }
}