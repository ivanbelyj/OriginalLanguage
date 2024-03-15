import { SignalRProvider } from "../chats/SignalRContext";
import Chat from "../chats/components/Chat";

export default function MainPage() {
  return (
    <>
      <SignalRProvider>
        <Chat />
      </SignalRProvider>
    </>
  );
}
