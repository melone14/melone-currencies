import { StyledButton, Wrapper } from './PaginationControls.style';

interface PaginationControlsProps {
  pageNumber: number;
  totalPages: number;
  totalCount: number;
  onPageChange: (page: number) => void;
}

export function PaginationControls({
  pageNumber,
  totalPages,
  totalCount,
  onPageChange,
}: PaginationControlsProps) {
  return (
    <Wrapper>
      <StyledButton
        onClick={() => onPageChange(Math.max(pageNumber - 1, 1))}
        disabled={pageNumber <= 1}
      >
        Poprzednia
      </StyledButton>

      <span>
        Strona {pageNumber} z {totalPages} ({totalCount} walut)
      </span>

      <StyledButton
        onClick={() => onPageChange(Math.min(pageNumber + 1, totalPages))}
        disabled={pageNumber >= totalPages}
      >
        Następna
      </StyledButton>
    </Wrapper>
  );
}
