import styled, { keyframes } from 'styled-components';

export const rotate = keyframes`
  100% {
    transform: rotate(360deg);
  }
`;

export const dash = keyframes`
  0% {
    stroke-dasharray: 1, 150;
    stroke-dashoffset: 0;
  }

  50% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -35;
  }

  100% {
    stroke-dasharray: 90, 150;
    stroke-dashoffset: -124;
  }
`;

export const Spinner = styled.svg`
  animation: ${rotate} 2s linear infinite;

  circle {
    fill: none;
    stroke: #383b4a;
    stroke-width: 5;
    stroke-linecap: round;
    animation: ${dash} 1.5s ease-in-out infinite;
  }
`;

export const Wrapper = styled.div`
  width: 200px;
  height: 498px;
  margin: auto;
  display: flex;
  align-items: center;

  & svg {
    color: #383b4a;
  }
`