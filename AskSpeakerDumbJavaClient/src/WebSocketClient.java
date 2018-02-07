import javax.websocket.*;
import javax.websocket.server.ServerEndpointConfig;
import java.net.URI;

@ClientEndpoint
public class WebSocketClient {
    private final String Uri = "ws://localhost:10000/any_path";
    private Session session;

    public WebSocketClient(){
        try{
            ServerEndpointConfig.Configurator c = new ServerEndpointConfig.Configurator();

            WebSocketContainer container = ContainerProvider.getWebSocketContainer();
            session = container.connectToServer(this, new URI(Uri));
        } catch (Exception ex){
            System.out.println("[ERROR]");
            System.out.println(ex.getMessage());
        }
    }

    @OnOpen
    public void onOpen(Session s){
        s.getAsyncRemote().sendText("Hello there from me!");
        System.out.println("Hello World!");
    }


    @OnMessage
    public void onMessage(String message){
        System.out.println("Recieved message: " + message);
    }

    @OnClose
    public void onClose(){
        System.out.println("Server closed!");
    }
}
