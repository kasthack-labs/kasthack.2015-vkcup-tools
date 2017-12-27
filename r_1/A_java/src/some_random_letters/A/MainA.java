package some_random_letters.A;

import java.io.*;

public class MainA {
    public static void main(String[] args) throws IOException {
        try (
                BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
                PrintWriter out = new PrintWriter(new OutputStreamWriter(System.out))
        ) {
            make2(in,out);
        }
    }

    public static void make2(BufferedReader in, PrintWriter out) {
        long time = System.currentTimeMillis();
        make(in, out);
        System.out.println(System.currentTimeMillis() - time);
    }

    public static void make(BufferedReader in, PrintWriter out) {

    }
}