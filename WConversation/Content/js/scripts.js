function scrollDownChat(){
	var objDiv = document.getElementById("divPosts");
	objDiv.scrollTop = objDiv.scrollHeight;
}


function startChat(){
	var msg = encodeURI(document.getElementById('name').value);
	document.location = "chat?name=" + msg;
}

function SendMessage()
{
	var msg = encodeURI(document.getElementById('watson_input').value);
	var username = document.getElementById('hiddenUsername').value;
	var chatbot = document.getElementById('divPosts');
	var xhttp = new XMLHttpRequest();
	xhttp.onreadystatechange = function() {
		
		if (this.readyState == 4 && this.status == 200) {
		    if (this.response == "redirect_pago") {
		        chatbot.innerHTML = chatbot.innerHTML + "Ser�s redireccionado";
		        document.location = 'https://escritorio.aspel.com.mx/webapps/CotizadorSist.exe/Cotizador';
		        scrollDownChat();
		    }
		    else {
		        chatbot.innerHTML = chatbot.innerHTML + this.responseText;
		        scrollDownChat();
		        document.getElementById('watson_input').value = "";
		    }
		}
	};
	xhttp.open("GET", "/api/SendMessage?id=" + msg, true);
	xhttp.send();
}

function RestartChat(userName){
	document.location = "/";
}

function InitializeConversation()
{
	var chatbot = document.getElementById('divPosts');
	var username = document.getElementById('hiddenUsername');
	var xhttp = new XMLHttpRequest();
	var url = document.location.toString();
	
	username.value = url.substr(url.indexOf("?")+6);

	xhttp.onreadystatechange = function() {
		
		if (this.readyState == 4 && this.status == 200) {
			chatbot.innerHTML = chatbot.innerHTML + this.responseText;
			scrollDownChat();
		}
	};
	xhttp.open("GET", "/api/Initialize", true);
	xhttp.send();

}