import javax.websocket.*;
import java.net.URI;

@ClientEndpoint
public class WebSocketClient {
    private final String Uri = "ws://localhost:10000/ClientRequest";

    public WebSocketClient(){
        try{
            WebSocketContainer container = ContainerProvider.getWebSocketContainer();
            container.connectToServer(this, new URI(Uri));
        } catch (Exception ex){
            System.out.println("[ERROR]");
            System.out.println(ex.getMessage());
        }
    }

    @OnOpen
    public void onOpen(){
        System.out.println("Hello World!");
    }

    @OnMessage
    public void onMessage(String message){
        System.out.println("Recieved message: " + message);
    }
}
