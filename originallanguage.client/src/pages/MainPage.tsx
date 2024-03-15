import Chat from "../chats/components/Chat";

export default function MainPage() {
  return (
    <>
      <div>{import.meta.env.VITE_API_URL}</div>;
      <Chat />
    </>
  );
}
