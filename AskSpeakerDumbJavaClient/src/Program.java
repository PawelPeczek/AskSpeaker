import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class Program {
    public static void main(String[] args) throws IOException {
        WebSocketClient ws = new WebSocketClient();

        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        br.readLine();
    }
}
