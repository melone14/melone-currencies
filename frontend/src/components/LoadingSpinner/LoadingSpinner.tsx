import { Spinner, Wrapper } from './LoadingSpinner.style';

export function LoadingSpinner() {
  return (
    <Wrapper>
      <Spinner viewBox="0 0 50 50">
        <circle cx="25" cy="25" r="20" />
      </Spinner>
    </Wrapper>
  );
}
