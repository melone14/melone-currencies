import styled from "styled-components";

export const Wrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 16px;
`;

export const StyledButton = styled.button`
  background: #272933;
  color: #9ca3af;
  border: none;
  padding: 8px 16px;

  &:disabled {
    filter: brightness(60%);
    cursor: not-allowed;
  }
`;