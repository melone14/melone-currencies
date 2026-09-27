interface ErrorMessageProps {
  message: string;
}

function ErrorMessage({ message }: ErrorMessageProps) {
  return <div>Błąd: {message}</div>;
}

export default ErrorMessage;