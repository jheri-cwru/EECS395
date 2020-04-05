import React from 'react';
import { Box, Flex, Image, Text} from 'rebass'
import './App.css';
import logo from './logo.svg'
import flowchart from './flowchart.svg'


function App() {
  return (
    <Box width='100%' height='70%'>
      <Flex flexDirection='column'>
        <Flex width='100%' >
          <Box width='50%'>
            <Image height='80%' paddingLeft='35%' paddingTop='20%' src={logo}></Image>
          </Box>
          <Box margin='20% auto'>
            <Flex flexDirection='column' >
                <Text textAlign='center' color='#505050' fontSize={[2, 4]} fontFamily='courier'>SECURE YOUR</Text>
                <Text textAlign='center' color='#0072C6' fontSize={[4, 8]} fontFamily='courier'>Comms</Text>
                <Text textAlign='center' fontSize={[1, 2]} fontFamily='courier'>Gain access to intelligent spam filtering</Text>
                <Text textAlign='center' fontSize={[1, 2]} fontFamily='courier'>and decentralized security.</Text>
                <Text textAlign='center' fontSize={[1, 2]} fontFamily='courier'>Add our extension to Outlook.</Text>
                <Text textAlign='center' fontSize={[1, 2]} fontFamily='courier'>For Free.</Text>
            </Flex>
          </Box>
        </Flex>
        <Flex marginBottom='100px'>
          <Box width='50%'>
            <Text marginLeft='100px' fontSize={[4, 8]} color='#0072c6' fontFamily='courier'>How it Works</Text>
            <br/>
            <Text marginLeft='100px' fontSize={[1, 3]} fontFamily='courier'>Simply add our extension to your outlook client and sign up. Once installed, your emails will be signed using public private key pairs. <br/><br/>
            Then, based on your actions, email addresses will automatically be added to white/black lists. For example, replying to a user a certain amount of times will add that user's public key to your white list.<br/><br/> 
            We keep none of your information except your public key and the keys you do/don't trust. </Text>
          </Box>
          <Box width='50%'>
            
            <Image width='90%' paddingLeft='10%' paddingTop='5%' src={flowchart}></Image>
          </Box>
        </Flex>
      </Flex>
    </Box>
  );
}

export default App;
