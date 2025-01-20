

const baseUrl = window.location.origin;
const wsUrl = baseUrl.replace( /^http/, 'ws' ); 

const socket = new WebSocket( wsUrl + "/ws" );
socket.onopen = () =>
{
    console.log( "WebSocket connection established!" );
   
};

socket.onmessage = ( event ) =>
{
    console.log( "Received from server:", event.data );

    const botMessage = document.createElement( 'div' );
    botMessage.className = 'bot-message';
    botMessage.innerHTML = event.data;
    messagesDiv.appendChild( botMessage );
};

const input = document.querySelector( '.message-input input' );
const sendButton = document.querySelector( '.send-btn' );
const messagesDiv = document.querySelector( '.messages' );

sendButton.addEventListener( 'click', sendMessage );
input.addEventListener( 'keypress', ( e ) =>
{
    if ( e.key === 'Enter' ) sendMessage();
} );

function sendMessage ()
{
    if ( !input.value.trim() ) return;

    // Display user message
    const userMessage = document.createElement( 'div' );
    userMessage.className = 'user-message';
    userMessage.innerHTML = `<p>${ input.value }</p>`;
    messagesDiv.appendChild( userMessage );
    socket.send( input.value );
    // Simulate bot response

    // Scroll to the bottom
    messagesDiv.scrollTop = messagesDiv.scrollHeight;

    input.value = '';
}