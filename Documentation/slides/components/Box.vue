<!--

Simple Box

Example usage:

<box x="100" y="140" width="100" height="50" color="green" borderwidth="3" />

Fallback color: red
Fallback borderwidth: 3

-->

<script setup lang="ts">

const props = defineProps<{
  x: number | string
  y: number | string
  width: number | string
  height: number | string
  borderwidth?: number
  color?: string
}>()

const ypx = props.y + "px"
const w1 = +props.x + +props.width + 50
const h1 = +props.y + +props.height + 10
const box = "0 0 " + w1 + " " + h1

</script>

<template>
  <svg
    class="absolute left-0 top-0 pointer-events-none"
    :width="+x + +width + 50"
    :height="+y + +height + 10"
    :viewBox="box"
  >
    <rect
        :x="x"
        :y="ypx"
        :width="width" 
        :height="height"
        :stroke="color || 'red'" 
        :stroke-width="borderwidth || 3"
        style="fill-opacity:0" />
  </svg>
</template>
