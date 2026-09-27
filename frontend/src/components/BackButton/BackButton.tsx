import { useNavigate } from 'react-router-dom';
import { BackButtonWrapper } from './BackButton.style';

export function BackButton() {
  const navigate = useNavigate();

  return (
    <BackButtonWrapper onClick={() => navigate(-1)}>
      ← Wróć do listy
    </BackButtonWrapper>
  );
}
